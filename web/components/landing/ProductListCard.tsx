import Image from "next/image";
import { AtSign, Facebook, MapPin, SendIcon, Twitter } from "lucide-react";
import { Badge } from "../ui/badge";
import { ProductType } from "@/lib/type";
import Link from "next/link";

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

export default function ProductListCard({
  product,
}: {
  product?: ProductType;
}) {
  const { title, images, city, price, description, colors, sizes } =
    product || defaultProduct;
  return (
    <div className="flex gap-x-5 group relative">
      <div className="max-w-[300px] aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-surfaceContainerLow dark:bg-black lg:aspect-none group-hover:opacity-75 lg:h-80">
        <Image
          src={images[0]?.imageUrl || "/products/4.png"}
          alt={title || "Product Name"}
          className="h-full w-full object-cover object-center lg:h-full lg:w-full"
          width={500}
          height={500}
        />
      </div>
      <div className="flex flex-col items-start justify-center gap-y-5 w-full">
        <div className="flex flex-col justify-start items-start">
          <p className="text-lg font-semibold">{title || "Product Name"}</p>
          <Link href="/product">
            <span aria-hidden="true" className="absolute inset-0" />
          </Link>
          <p className="text-sm text-Outline">{description || "description"}</p>
        </div>
        <div>
          <p className="text-xl text-pink-600">ETB {price || 20}</p>
        </div>
        <div className="flex flex-col gap-y-6 text-sm">
          <div className="flex gap-x-4  justify-center items-center">
            <p>Color</p>
            {colors.map((color) => (
              <div
                key={color.id}
                className={`w-5 h-5 rounded-full border border-opacity-25 border-onSurface`}
                style={{ backgroundColor: color.hexCode }}
              ></div>
            ))}
          </div>
        </div>
        <div className="flex gap-x-5 justify-center items-center">
          <p>Size</p>
          <div className="flex gap-x-5 items-center w-full">
            {sizes.map((size) => (
              <div
                key={size.id}
                className="w-9 h-9 flex justify-center items-center rounded-full"
              >
                <Badge variant="outline">
                  <p className="text-xs">{size.abbreviation}</p>
                </Badge>
              </div>
            ))}
          </div>
        </div>
        <div className="flex gap-x-1 justify-start items-center">
          <MapPin className="w-5 h-5 opacity-75" />
          <p>{city}</p>
        </div>
      </div>
    </div>
  );
}
