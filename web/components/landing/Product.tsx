import React from "react";
import Image from "next/image";


type props = {
    image: string;
    Name: string;
    Price: number;
}

const Product = ({image, Name, Price} : props) => {
  return (
    <div className="flex flex-col justify-center items-start text-black gap-5">
      <div className="flex flex-col justify-center items-center p-10 gap-2 bg-[#F7FBFD] shadow-md">
        <Image src={image} alt="Product" width={600} height={600} />
      </div>
      <div>
        <p>{Name}</p>
        <p>
          Price: <span className="text-[#EE1E80]">${Price}</span>
        </p>
      </div>
    </div>
  );
};

export default Product;
