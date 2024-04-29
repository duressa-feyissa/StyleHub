import React from "react";
import Image from "next/image";
import Product from "../../components/landing/Product";

const page = () => {
  return (
    <div className="flex flex-col p-16 text-black gap-10">
      <div className="flex justify-between items-center py-16">
        <div>
          <p className="font-Poppins">
            Home {">>"} <span className="text-[#EE1E80]">Product</span>
          </p>
        </div>
        <div className="flex items-center justify-center gap-2 ">
          <p>find anything in </p>
          <div className="bg-black text-white px-5 py-2">
            <p>Ethiopia</p>
          </div>
        </div>
      </div>
      <div className="flex justify-between items-center p-5 bg-[#FFF4EC]">
        <p>Filter</p>
        <p>Sort by</p>
      </div>
      <div className="grid grid-cols-4 grid-rows-3 gap-10 mb-8">
        <Product
          Name="Elegant Beige Blazar For Women"
          Price={128}
          image="/images/Elegant.png"
        />
        <Product
          Name="Men Fashion Clothing Set"
          Price={248}
          image="/images/Men.png"
        />
        <Product
          Name="Yellow Summer Dress"
          Price={61}
          image="/images/Yellow.png"
        />
        <Product
          Name="White Hoodle Cutout"
          Price={64}
          image="/images/White.png"
        />
        <Product
          Name="Women Dress Shoes"
          Price={44}
          image="/images/Women.png"
        />
        <Product
          Name="Stylish Bespoke Jacket"
          Price={44}
          image="/images/Stylish.png"
        />
        <Product Name="Black T-Shirt" Price={34} image="/images/Black.png" />
        <Product
          Name="Gold glittery sequin party top"
          Price={74}
          image="/images/Gold.png"
        />
        <Product
          Name="White Pullover Cutout"
          Price={64}
          image="/images/Pullover.png"
        />
        <Product
          Name="Style Dress for Girls"
          Price={140}
          image="/images/Style.png"
        />
        <Product
          Name="Men's Classic Blue Folded Cotton Shirt"
          Price={56}
          image="/images/Classic.png"
        />
        <Product Name="Women Dress" Price={49} image="/images/Dress.png" />
      </div>
    </div>
  );
};

export default page;
