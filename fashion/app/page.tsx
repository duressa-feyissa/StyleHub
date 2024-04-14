import Footer from "@/components/common/Footer";
import Navbar from "@/components/common/Navbar";
import Category from "@/components/landing/Category";
import Hero from "@/components/landing/Hero";
import Product from "@/components/landing/Product";
import Image from "next/image";

export default function Home() {
  return (
    <>
      <div className="">
        <Hero />
        <Category />
        <Product />
        <Footer />
      </div>
    </>
  );
}
