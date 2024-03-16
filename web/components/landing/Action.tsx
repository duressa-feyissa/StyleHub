"use client";

import React from "react";
import { useState, useEffect } from "react";
import Image from "next/image";
import Button from "../common/Button";

const Images = [
  {
    image: "/images/img-1.png",
    width: 400,
    height: 100,
  },
  {
    image: "/images/img-2.png",
    width: 400,
    height: 100,
  },
];

const Action = () => {
  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImageIndex((prevIndex) => (prevIndex + 1) % 2);
    }, 3000);

    return () => clearInterval(interval);
  }, []);

  const getCurrentImage = () => {
    return Images[currentImageIndex];
  };

  return (
    <div
      className={`flex justify-center items-center font-Poppins ${
        currentImageIndex === 1
          ? "flex-row-reverse gap-32 bg-[#FFFDEC]"
          : "gap-40"
      } w-screen pt-16 bg-[#F4F2FA]`}
      key={currentImageIndex}
    >
      <div>
        <Image
          src={getCurrentImage().image}
          width={getCurrentImage().width}
          height={getCurrentImage().height}
          alt="foreground"
        />
      </div>
      <div className="w-[580px] flex flex-col justify-center items-start gap-y-5">
        <p className="text-[#06164B] text-4xl font-bold">
          Empower Your Style: Unlock Your{" "}
          <span className="text-[#EE1E80]">Best Look</span> with Us
        </p>
        <p className="text-[#06164B] text-xl">
          Elevate Your Wardrobe: Explore the Latest Fashion Trends and Must-Have
          Styles! Explore Now on StyleHub.
        </p>
        <div className="my-4">
          <Button
            className="bg-[#EE1E80] rounded-full px-5 py-3 text-white"
            Name="View All Products"
          />
        </div>
      </div>
      {/* {currentImageIndex === 1 ? (
        <div className="flex-1"></div>
      ) : null
      } */}
    </div>
  );
};

export default Action;
