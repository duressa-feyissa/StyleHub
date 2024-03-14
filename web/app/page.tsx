import Image from "next/image";
import Navbar from "../components/common/Navbar";
import Header from "../components/landing/Header";
import Footer from "../components/common/Footer";

export default function Home() {
  return (
    <main className="flex max-w-screen min-h-screen flex-col items-center justify-between p-24">
      <Header />
    </main>
  );
}
