"use client";

import React, { useState, useEffect, Component } from "react";
import Image from "next/image";
import Button from "../common/Button";

const Images = [
  {
    ellipse: "/images/Ellipse.svg",
    image: "/images/image-2.png",
    para: "Discover Limitless Style",
    Head1: "FASHION",
    Head2: "HEAVEN",
    sub: "Indulge in Endless Style Possibilities at StyleHub's Fashion Haven, where curated collections and trendsetting designs await to elevate your wardrobe.",
    width: 500,
    height: 200,
  },
  {
    ellipse: "/images/Ellipse (1).svg",
    image: "/images/image.png",
    para: "",
    Head1: "MASCULINE",
    Head2: "STYLES",
    sub: "Immerse Yourself in Sophisticated Men's Collections at StyleHub, where meticulously curated fashion awaits to refine your wardrobe with timeless flair and trendsetting allu",
    width: 700,
    height: 200,
  },
  {
    ellipse: "/images/Ellipse (2).svg",
    image: "/images/image-3.png",
    para: "",
    Head1: "WOMEN'S",
    Head2: "COLLECTION",
    sub: "Dive into Stylish Women's Collections at StyleHub, where curated fashion awaits to enhance your wardrobe with timeless elegance and trendsetting designs.",
    width: 400,
    height: 200,
  },
];

const Header = () => {
  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImageIndex((prevIndex) => (prevIndex + 1) % 3);
    }, 3000);

    return () => clearInterval(interval);
  }, []);

  const getCurrentImage = () => {
    return Images[currentImageIndex];
  };

  return (
    <div
      className={`flex  ${
        currentImageIndex === 1 ? "flex-row-reverse gap-0" : "gap-72"
      } w-screen h-[637px] bg-[#F4F2FA] transition-all duration-500 ease-in-out`}
      key={currentImageIndex}
    >
      <div
        className={`relative ${
          currentImageIndex === 1 ? "left-0" : "left-40"
        } top-16 transition-all duration-500 ease-in-out`}
      >
        <div
          className={`z-0 ${
            currentImageIndex === 1
              ? "right-16 bottom-0"
              : currentImageIndex === 2
              ? "right-16 bottom-8"
              : "right-10 bottom-8"
          } relative transition-all duration-500 ease-in-out`}
        >
          <Image
            src={getCurrentImage().ellipse}
            width={600}
            height={200}
            alt="background"
          />
        </div>
        <div
          className={`z-40 ${
            currentImageIndex === 1
              ? "right-24 bottom-[620px]"
              : currentImageIndex === 2
              ? "left-5 bottom-[600px]"
              : "right-10 bottom-[600px]"
          } relative transition-all duration-500 ease-in-out`}
        >
          <Image
            src={getCurrentImage().image}
            width={getCurrentImage().width}
            height={getCurrentImage().height}
            alt="foreground"
          />
        </div>
      </div>
      <div className="w-[600px] flex flex-col justify-center items-start gap-y-5">
        <p className="text-[#06164B] text-2xl">{getCurrentImage().para}</p>
        <p className="text-[#06164B] text-6xl font-bold font-Poppins">
          {getCurrentImage().Head1}{" "}
          <span className="text-[#EE1E80] text-">
            {getCurrentImage().Head2}
          </span>
        </p>
        <p className="text-[#5A5D72] w-[480px]">{getCurrentImage().sub}</p>
        <div className="my-8">
          <Button
            className="bg-[#EE1E80] rounded-full px-5 py-3 text-white"
            Name="Explore Now"
          />
        </div>
      </div>
    </div>
  );
};

export default Header;