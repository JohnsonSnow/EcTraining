// utils/holidayHandler.ts
import { z } from 'zod';
import dayjs from 'dayjs';
import { holidaySchema, HolidayFormValues } from './validation';

export const bookHolidayAndExtendCourse = (
  courseEndDate: string, 
  holidayStartDate: string, 
  holidayEndDate: string
): string => {
  // Validate holiday dates
  const parsedHoliday = holidaySchema.safeParse({ holidayStartDate, holidayEndDate });

  if (!parsedHoliday.success) {
    throw new Error(parsedHoliday.error.issues.map(issue => issue.message).join(', '));
  }

  const holidayDuration = dayjs(holidayEndDate).diff(dayjs(holidayStartDate), 'week') + 1; // Duration in weeks
  const newCourseEndDate = dayjs(courseEndDate).add(holidayDuration, 'week').format('YYYY-MM-DD');

  return newCourseEndDate;
};
