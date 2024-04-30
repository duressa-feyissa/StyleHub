import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import Image from "next/image";
import image from "../../public/hero/Image-2.png";
import ProductCard from "../../components/landing/ProductCard";
import { AtSign, Facebook, MapPin, SendIcon, Twitter } from "lucide-react";

const product = {
  id: 1,
  name: "Basic Tee",
  href: "#",
  imageSrc: "/products/5.png",
  imageAlt: "Front of men's Basic Tee in black.",
  price: "35",
  location: "Addis Ababa, 4 kilo",
};

export default function Page() {
  return (
    <div className="w-full flex flex-col items-center justify-center gap-y-24 pt-10 lg:container">
      <div className="flex justify-center items-center gap-x-24">
        <div className="flex gap-x-10">
          <div className="flex flex-col gap-y-4 justify-center items-center">
            <Image src={image} alt="image" width={233} height={233} />
            <Image src={image} alt="image" width={233} height={233} />
            <Image src={image} alt="image" width={233} height={233} />
          </div>
          <Image src={image} alt="image" width={500} height={500} />
        </div>
        <div className="flex flex-col items-start justify-start gap-y-5 w-1/2">
          <div className="flex flex-col justify-start items-start gap-y-4 pr-44">
            <p className="text-2xl font-semibold">White Hoodle Cutout</p>
            <p className="text-md text-Outline">
              The White Hoodie Cutout is a modern twist on a classic wardrobe
              staple, featuring unique cutout details that add an edgy and
              contemporary flair to your casual look.
            </p>
          </div>
          <div>
            <p className="text-3xl text-pink-600">ETB 34</p>
          </div>
          <div className="flex flex-col gap-y-6 text-sm">
            <div className="flex gap-x-5">
              <p>brand</p>
              <p className="font-semibold">Adidas</p>
            </div>
            <div className="flex gap-x-4">
              <p>Color</p>
              {["black", "green", "red"].map((color) => (
                <div
                  key={color}
                  className={`w-5 h-5 rounded-full border border-opacity-25 border-onSurface`}
                  style={{ backgroundColor: color }}
                ></div>
              ))}
            </div>
            <div className="flex gap-x-5">
              <p>Category</p>
              <p className="font-semibold">Apparel, Tops, Sweaters</p>
            </div>
            <div className="flex gap-x-5">
              <p>Size</p>
              <div className="flex gap-x-5 items-center w-full">
                {["S", "M", "L", "XL", "XXL", "XXXL"].map((size) => (
                  <div
                    key={size}
                    className="bg-surfaceContainerLow w-10 h-10 flex justify-center items-center rounded-full"
                  >
                    <p>{size}</p>
                  </div>
                ))}
              </div>
            </div>
          </div>
          <div className="flex flex-col gap-y-3 text-sm">
            <div className="flex justify-center items-center gap-x-5">
              <p>Share Link</p>
              <div className="flex gap-x-5">
                {[
                  { social: "Twitter", icon: Twitter },
                  { social: "Facebook", icon: Facebook },
                  { social: "Telegram", icon: SendIcon },
                  { social: "Mail", icon: AtSign },
                ].map(({ social, icon: IconComponent }) => (
                  <button
                    key={social}
                    className="flex justify-center items-center rounded-full"
                  >
                    <IconComponent className="opacity-75 w-5 h-5" />
                  </button>
                ))}
              </div>
            </div>
            <div className="flex gap-x-1 justify-start items-center">
              <MapPin className="w-5 h-5 opacity-75" />
              <p>Addis Ababa, Ethiopia</p>
            </div>
          </div>
        </div>
      </div>
      <div className="w-full pb-24 lg:container">
        <Carousel>
          <CarouselContent className="w-full gap-x-2">
            {[1, 2, 3, 4, 5, 6, 7, 8].map((index) => (
              <CarouselItem key={index} className="md:basis-1/2 lg:basis-1/4">
                <ProductCard
                  {...product}
                  imageSrc={`/products/${index + 4}.png`}
                />
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious />
          <CarouselNext />
        </Carousel>
      </div>
    </div>
  );
}
