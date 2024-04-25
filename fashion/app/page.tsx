import Category from "@/components/landing/Category";
import Hero from "@/components/landing/Hero";
import Product from "@/components/landing/Product";

export default function Home() {
  return (
    <>
      <div className="">
        <Hero />
        <Category />
        <Product />
      </div>
    </>
  );
}
