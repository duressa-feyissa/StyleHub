import React from "react";
import Image from "next/image";
import Button from "./Button";

const Navbar = () => {
  return (
    <nav className="bg-white fixed w-full z-50 top-0 border-b shadow-lg font-[Poppins]">
      <div className="flex flex-wrap font-medium items-center justify-between max-auto px-24 text-sm">
        <Image
          src="/logo.svg"
          width={100}
          height={90}
          alt="logo"
        />
        <input
          className="w-96 h-10 text-black font-Poppins rounded-full px-4 bg-[#FBF8FF]"
          type="text"
          placeholder="Search"
        />
        <div className="flex items-center gap-x-6">
          <Button Name="Deal Now" className="text-[#EE1E80] border-2 border-[#EE1E80] rounded-full px-5 py-2" />
          <Button Name="Buy Now" className="bg-[#EE1E80] rounded-full px-5 py-2" />
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
