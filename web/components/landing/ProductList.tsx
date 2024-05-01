import Image from "next/image";
import ProductListCard from "@/components/landing/ProductListCard";

export default function page() {
  return (
    <div className="pt-10 flex flex-col gap-y-10">
      <ProductListCard />
      <ProductListCard />
      <ProductListCard />
      <ProductListCard />
      <ProductListCard />
      <ProductListCard />
    </div>
  );
}
