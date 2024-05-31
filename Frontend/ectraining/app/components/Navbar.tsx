import Link from "next/link";

export function Navbar() {
    return (
        <nav className="relative max-w-7xl w-full md:grid md:grid-cols-12 items-center px-4 md:px-8 mx-auto py-7">
            <div className="md:col-span-3">
                <Link href="/">
                    <h1 className="text-2xl font-semibold"><span className="text-violet-500">EC</span>Learning</h1>
                </Link>
            </div>
        </nav>
    );
}