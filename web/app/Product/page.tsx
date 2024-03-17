import React from 'react'
import Image from "next/image"; 
import Product from "../../components/landing/Product";

const page = () => {
  return (
      <div className="flex flex-col max-w-screen min-h-screen items-center justify-between p-24">
        <div className="p-20 ">
            <p className="text-4xl text-black font-semibold font-Poppins">
                FEATURED ITEMS
            </p>
        </div>
        <div className="grid grid-cols-3 grid-rows-2 gap-10 mb-8">
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
}

export default page