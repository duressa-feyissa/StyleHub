import Image from "next/image";
import ProductListCard from "@/components/landing/ProductListCard";
import ProductCard from "./ProductCard";
import { ProductType } from "@/lib/type";
import { useGetProducts } from "@/lib/data/get-products";

export default function Products({
  list = true,
  cols = 4,
}: {
  list?: boolean;
  cols?: number;
}) {
  const { data: products, error: postError, fetchStatus } = useGetProducts();
  if (postError || !products || products.length <= 0)
    return list ? (
      <div className="pt-10 flex flex-col gap-y-10">
        {[1, 2, 3, 4, 5, 6, 7, 8].map((product) => (
          <ProductListCard key={product} />
        ))}
      </div>
    ) : (
      <div
        className={`pt-10 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-${cols} xl:gap-x-8`}
      >
        {[1, 2, 3, 4, 5, 6, 7, 8].map((product) => (
          <ProductCard key={product} />
        ))}
      </div>
    );
  return list ? (
    <div className="pt-10 flex flex-col gap-y-10">
      {products.map((product: ProductType) => (
        <ProductListCard product={product} key={product.id} />
      ))}
    </div>
  ) : (
    <div
      className={`pt-10 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-${cols} xl:gap-x-8`}
    >
      {products.map((product: ProductType) => (
        <ProductCard product={product} key={product.id} />
      ))}
    </div>
  );
}
