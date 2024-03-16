import React from "react";
import Image from "next/image";

type props = {
  Name: string;
  image: string;
};

const Category = ({ Name, image }: props) => {
  return (
    <div className="flex flex-col justify-center items-center p-10 gap-2 shadow-md bg-[#F7FBFD]">
      <p className="text-2xl text-[#EE1E80]">{Name}</p>
      <Image src={image} alt={Name} width={400} height={400} />
    </div>
  );
};

export default Category;
