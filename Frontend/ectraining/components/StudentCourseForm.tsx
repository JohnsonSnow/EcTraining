'use client'
import React, { useState, useEffect } from 'react';
import { z } from 'zod';
import dayjs from 'dayjs';

interface CourseOption {
  courseId: string;
  name: string;
  description: string;
}

// Utility functions for date validation
const isMonday = (date: string): boolean => dayjs(date).day() === 1;
const isFriday = (date: string): boolean => dayjs(date).day() === 5;

// Course schema
const courseSchema = z.object({
  studentName: z.string().min(1, 'Full name is required'),
  studentEmail: z.string().email('Invalid email address'),
  courses: z.array(
    z.object({
      courseName: z.string().min(1, 'Course name is required'),
      startDate: z.string().refine((date) => dayjs(date).isValid(), {
        message: 'Invalid start date',
      }).refine(isMonday, { message: 'Course must start on a Monday' }),
      endDate: z.string().refine((date) => dayjs(date).isValid(), {
        message: 'Invalid end date',
      }).refine(isFriday, { message: 'Course must end on a Friday' }),
    })
  ).refine((courses) => {
    // Check for overlapping courses
    for (let i = 0; i < courses.length - 1; i++) {
      if (dayjs(courses[i].endDate).isAfter(dayjs(courses[i + 1].startDate))) {
        return false;
      }
    }
    return true;
  }, { message: 'Courses cannot overlap' }),
});

type CourseFormValues = z.infer<typeof courseSchema>;

const StudentCourseForm: React.FC = () => {
  const [formData, setFormData] = useState<CourseFormValues>({
    studentName: '',
    studentEmail: '',
    courses: [{ courseName: '', startDate: '', endDate: '' }],
  });

  const [errors, setErrors] = useState<Partial<z.inferFlattenedErrors<typeof courseSchema>>>({ fieldErrors: {} });
  const [courseOptions, setCourseOptions] = useState<CourseOption[]>([]);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const response = await fetch('https://localhost:5301/api/Course/GetAllCourse');
        const data = await response.json();
        if (data.isSuccess) {
          setCourseOptions(data.value);
          console.log(data.value);
          console.log(courseOptions);
        } else {
          console.error('Failed to fetch courses', data.error);
        }
      } catch (error) {
        console.error('Failed to fetch courses', error);
      }
    };

    fetchCourses();
  }, []);

  useEffect(() => {
    validateForm();
  }, [formData]);

  const validateForm = () => {
    try {
      courseSchema.parse(formData);
      setErrors({ fieldErrors: {} });
    } catch (e) {
      if (e instanceof z.ZodError) {
        setErrors(e.flatten());
      }
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>, index?: number) => {
    const { name, value } = e.target;
    if (index !== undefined) {
      const updatedCourses = [...formData.courses];
      updatedCourses[index] = { ...updatedCourses[index], [name]: value };
      setFormData({ ...formData, courses: updatedCourses });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const addCourse = () => {
    // Validate the current form data before adding a new course
    try {
      courseSchema.parse(formData);
      setFormData({ ...formData, courses: [...formData.courses, { courseName: '', startDate: '', endDate: '' }] });
    } catch (e) {
      if (e instanceof z.ZodError) {
        setErrors(e.flatten());
      }
    }
  };

  const removeCourse = (index: number) => {
    const updatedCourses = formData.courses.filter((_, i) => i !== index);
    setFormData({ ...formData, courses: updatedCourses });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      courseSchema.parse(formData);
      console.log(courseOptions);
      
      // Transform formData to the expected API format
      const transformedData = {
        fullname: formData.studentName,
        emailAdress: formData.studentEmail,
        enrollments: formData.courses.map(course => {
          const selectedCourse = courseOptions.find(option => option.name === course.courseName);
          console.log(selectedCourse);
          return {
            courseId: selectedCourse?.courseId ?? '',
            startDate: new Date(course.startDate).toISOString(),
            endDate: new Date(course.endDate).toISOString(),
          };
        }),
      };

      const response = await fetch('https://localhost:5301/api/Student/StudentCourseEnrollment', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'accept': '*/*',
        },
        body: JSON.stringify(transformedData),
      });

      if (response.ok) {
        console.log('Form submitted successfully');
      } else {
        console.error('Failed to submit form');
      }
    } catch (e) {
      if (e instanceof z.ZodError) {
        setErrors(e.flatten());
        console.log('Form data is invalid:', e.errors);
      }
    }
  };


  return (
    <form onSubmit={handleSubmit} className="space-y-4 p-4">
      <div className="space-y-2">
        <label className="block text-sm font-medium text-gray-700">Student Full Name</label>
        <input
          type="text"
          name="studentName"
          value={formData.studentName}
          onChange={handleChange}
          className="w-full px-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300"
        />
        {errors.fieldErrors?.studentName && <p className="text-red-500 text-sm">{errors.fieldErrors.studentName}</p>}
      </div>
      <div className="space-y-2">
        <label className="block text-sm font-medium text-gray-700">Student Email Address</label>
        <input
          type="email"
          name="studentEmail"
          value={formData.studentEmail}
          onChange={handleChange}
          className="w-full px-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300"
        />
        {errors.fieldErrors?.studentEmail && <p className="text-red-500 text-sm">{errors.fieldErrors.studentEmail}</p>}
      </div>
      {formData.courses.map((course, index) => (
        <div key={index} className="space-y-2">
          <div className="flex items-center space-x-4">
            <div className="flex-1">
              <label className="block text-sm font-medium text-gray-700">Course Name</label>
              <select
                name="courseName"
                value={course.courseName}
                onChange={(e) => handleChange(e, index)}
                className="w-full px-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300"
              >
                <option value="">Select a course</option>
                {courseOptions.map((option) => (
                  <option key={option.courseId} value={option.courseId}>{option.name}</option>
                ))}
              </select>
              {errors.fieldErrors?.courses?.[index]?.courseName && <p className="text-red-500 text-sm">{errors.fieldErrors.courses[index]?.courseName}</p>}
            </div>
            <div className="flex-1">
              <label className="block text-sm font-medium text-gray-700">Course Start Date</label>
              <input
                type="date"
                name="startDate"
                value={course.startDate}
                onChange={(e) => handleChange(e, index)}
                className="w-full px-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300"
              />
              {errors?.fieldErrors?.courses?.[index]?.startDate && <p className="text-red-500 text-sm">{errors.fieldErrors.courses[index]?.startDate}</p>}
            </div>
            <div className="flex-1">
              <label className="block text-sm font-medium text-gray-700">Course End Date</label>
              <input
                type="date"
                name="endDate"
                value={course.endDate}
                onChange={(e) => handleChange(e, index)}
                className="w-full px-4 py-2 border rounded focus:outline-none focus:ring focus:border-blue-300"
              />
              {errors?.fieldErrors?.courses?.[index]?.endDate && <p className="text-red-500 text-sm">{errors.fieldErrors.courses[index]?.endDate}</p>}
            </div>
            <button type="button" onClick={() => removeCourse(index)} className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-700">Remove</button>
          </div>
        </div>
      ))}
      <button type="button" onClick={addCourse} className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 mr-2">Add Course</button>
      <button type="submit" className="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-700">Submit</button>
    </form>
  );
};

export default StudentCourseForm;
