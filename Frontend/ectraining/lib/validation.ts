// utils/validation.ts
import { z } from 'zod';
import dayjs from 'dayjs';

// Utility functions for date validation
const isMonday = (date: string): boolean => dayjs(date).day() === 1;
const isFriday = (date: string): boolean => dayjs(date).day() === 5;

// Course schema
export const courseSchema = z.object({
  studentName: z.string().min(1, 'Full name is required'),
  studentEmail: z.string().email('Invalid email address'),
  courses: z.array(
    z.object({
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

// Holiday schema
export const holidaySchema = z.object({
  holidayStartDate: z.string().refine((date) => dayjs(date).isValid(), {
    message: 'Invalid holiday start date',
  }).refine(isMonday, { message: 'Holiday must start on a Monday' }),
  holidayEndDate: z.string().refine((date) => dayjs(date).isValid(), {
    message: 'Invalid holiday end date',
  }).refine(isFriday, { message: 'Holiday must end on a Friday' }),
});

// TypeScript types from Zod schemas
export type CourseFormValues = z.infer<typeof courseSchema>;
export type HolidayFormValues = z.infer<typeof holidaySchema>;
