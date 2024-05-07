import { ProductType } from "@/lib/type";
import Image from "next/image";
import Link from "next/link";
import { DirectionAwareHover } from "../ui/direction-aware-hover";
const defaultProduct = {
  title: "Product Name",
  images: [{ id: "1", imageUrl: "/products/9.png" }],
  city: "Addis Ababa, Ethiopia",
  price: 20,
  description:
    "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aperiam quia amet distinctio praesentium neque illum voluptas maxime magnam laborum repellat!",
  colors: [],
  sizes: [],
};

export default function ProductCard({ product }: { product?: ProductType }) {
  const { title, images, city, price } = product || defaultProduct;
  return (
    <div className="group relative">
      <div className="aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-surfaceContainerLow dark:bg-black lg:aspect-none group-hover:opacity-75 lg:h-80">
        <Image
          src={images[0]?.imageUrl || "/products/4.png"}
          alt={title}
          className="h-full w-full object-cover object-center lg:h-full lg:w-full"
          width={500}
          height={500}
        />
      </div>
      <div className="mt-4 flex justify-between">
        <div>
          <h3 className="text-lg font-medium text-gray-700 dark:text-white">
            <Link href="/product">
              <span aria-hidden="true" className="absolute inset-0" />
              {title}
            </Link>
          </h3>
          <p className="text-lg  font-bold text-primary">ETB {price}</p>
          <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">{city}</p>
        </div>
      </div>
    </div>
  );
}

export function ProductCard1({ product }: { product?: ProductType }) {
  const { title, images, city, price } = product || defaultProduct;
  return (
    <div className="group relative">
      <div className="relative  flex items-center justify-center">
        <DirectionAwareHover imageUrl="/products/7.png">
          <div className="mt-4 flex justify-between">
            <div>
              <h3 className="text-lg font-medium text-gray-700 dark:text-white">
                <Link href="/product">
                  <span aria-hidden="true" className="absolute inset-0" />
                  {title}
                </Link>
              </h3>
              <p className="text-lg  font-bold text-primary">ETB {price}</p>
              <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                {city}
              </p>
            </div>
          </div>
        </DirectionAwareHover>
      </div>
    </div>
  );
}