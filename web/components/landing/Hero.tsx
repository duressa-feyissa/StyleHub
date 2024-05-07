import Image from "next/image";
import image from "../../public/hero/Image-2.png";
import { Button } from "../ui/button";
import Link from "next/link";

const Hero = () => {
  return (
    <div className="bg-secondary content-end">
      <div className="w-full  flex flex-row justify-between lg:container max-h-screen py-32 lg:py-0">
        <div className="hidden md:w-[40%] md:block">
          <Image src={image} alt="slider" />
        </div>
        <div className="md:w-[45%] w-[70%] flex flex-col justify-center mx-auto md:justify-center items-center md:items-start justify-self-start">
          <div className="flex flex-col pb-10">
            <p className="">Discover Limitless Style</p>
            <Link className="font-bold font-Roboto text-6xl" href="/">
              FASHION{" "}
              <span className="text-primary font-bold text-6xl">HEAVEN</span>{" "}
            </Link>
            <p className="text-justify w-[85%] pt-4">
              Indulge in Endless Style Possibilities at StyleHub Fashion Haven,
              where curated collections and trendsetting designs await to
              elevate your wardrobe.
            </p>
          </div>
          <Button className="px-4">
            Explore Now
          </Button>
        </div>
      </div>
    </div>
  );
};

export default Hero;
