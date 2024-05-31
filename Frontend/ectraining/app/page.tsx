import StudentCourseForm from "@/components/StudentCourseForm";
import Image from "next/image";

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
       <div className="container mx-auto p-4">
          <h1 className="text-2xl font-bold mb-4">Student Course Form</h1>
          <StudentCourseForm />
        </div>
    </main>
  );
}
