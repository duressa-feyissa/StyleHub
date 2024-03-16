import Image from "next/image";
import Link from "next/link";
import Navbar from "../components/common/Navbar";
import Header from "../components/landing/Header";
import Footer from "../components/common/Footer";
import Category from "../components/landing/Category";
import Button from "../components/common/Button";
import Product from "../components/landing/Product";
import Action from "../components/landing/Action";

export default function Home() {
  return (
    <main className="flex max-w-screen min-h-screen flex-col items-center justify-between p-24">
      <Header />
      <div className="flex flex-col gap-16 pt-32">
        <p className="text-4xl text-black font-semibold font-Poppins">
          CATEGORY
        </p>
        <div className="flex gap-x-10">
          <Link href="/">
            <Category image="/images/Apparel.png" Name="Apparel" />
          </Link>
          <Link href="/">
            <Category image="/images/Footwear.png" Name="Footwear" />
          </Link>
          <Link href="/">
            <Category image="/images/Accessories.png" Name="Accessories" />
          </Link>
          <Link href="/">
            <Category image="/images/Sportswear.png" Name="Sportswear" />
          </Link>
          <Link href="/">
            <Category image="/images/Formalwear.png" Name="Formalwear" />
          </Link>
        </div>
      </div>
      <div className="flex flex-col pt-40 gap-16">
        <div className="flex justify-between items-center">
          <div>
            <p className="text-4xl text-black font-semibold font-Poppins">
              FEATURED ITEMS
            </p>
          </div>
          <div className="flex text-black gap-5">
            <Button className="text-black bg-white" Name="All " /> :
            <Button className="text-black bg-white" Name="MEN " /> :
            <Button className="text-black bg-white" Name="WOMEN " /> :
            <Button className="text-black bg-white" Name="SHOES " /> :
            <Button className="text-black bg-white" Name="ACCESSORIES " />
          </div>
        </div>
        <div className="grid grid-cols-3 grid-rows-2 gap-14 mb-32">
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
        </div>
      </div>
      <Action />
      <div className="flex flex-col pt-24 gap-10">
        <div className="flex justify-between items-center">
          <p className="text-4xl text-black font-semibold font-Poppins">
            TRENDING
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
    </main>
  );
}
