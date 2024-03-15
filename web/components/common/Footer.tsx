import React from "react";
import Image from "next/image";
import Link from "next/link";
import Button from "../common/Button";

const Footer = () => {
  return (
    <footer className="w-full bg-[#45464F] font-Poppins">
      <div className="flex justify-evenly items-start gap-10 py-32">
        <div className="flex flex-col items-start space-y-6 pl-32 px-10 ml-4 gap-4">
          <div className="flex flex-col gap-4 w-[390px]">
            <p className="text-4xl">Let{"'"}s stay in touch</p>
            <p className="text-white text-base w-[85%]">
              Subscribe to our newsletter to receive latest articles to your
              inbox weekly.
            </p>
          </div>
          <div className="flex items-center mt-2">
            <div className="flex">
              <input
                className="w-[250px] rounded-l-full px-5 py-3"
                type="text"
                placeholder="Email Address"
              />
              <Button
                Name="Subscribe"
                className="bg-[#EE1E80] px-5 py-3 text-white rounded-r-full"
              />
            </div>
          </div>
        </div>
        <div>
          <p className="text-4xl mb-5">Apps</p>
          <div className="flex justify-center items-center gap-4">
            <div className="w-[170px] gap-2 flex p-3 rounded-lg bg-[#161C24]">
              <Image
                src="/images/Appstore.svg"
                width={30}
                height={30}
                alt="appstore"
              />
              <div className="flex flex-col items-center justify-center">
                <p className="text-white text-xs">Download on the</p>
                <p className="text-white text-bold">Apple Store</p>
              </div>
            </div>
            <div className="w-[170px] flex p-3 gap-2 rounded-lg bg-[#161C24]">
              <Image
                src="/images/Playstore.svg"
                width={30}
                height={30}
                alt="playstore"
              />
              <div>
                <p className="text-white text-xs">Download from</p>
                <p className="text-white text-bold">Google Play</p>
              </div>
            </div>
          </div>
        </div>
        <div className="flex items-start gap-x-10 px-16 mr-10">
          <div className="flex flex-col items-start">
            <h2 className="text-white font-medium text-base text-center pb-3">
              About
            </h2>
            <ul className="text-white text-base space-y-3">
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">How it works</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Featured</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Partnership</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm w-32">Business Realtion</p>
                </Link>
              </li>
            </ul>
          </div>

          <div className="flex flex-col items-start w-28">
            <h2 className="text-white font-medium text-base text-center pb-3 w-fit">
              Community
            </h2>
            <ul className="text-white text-base space-y-3">
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Events</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Blog</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Podcast</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Teams</p>
                </Link>
              </li>
            </ul>
          </div>

          <div className="flex flex-col items-start w-28">
            <h2 className="text-white font-medium text-base text-center pb-3 w-fit">
              Socials
            </h2>
            <ul className="text-white text-base space-y-3">
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Discord</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Instagram</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Twitter</p>
                </Link>
              </li>
              <li>
                <Link
                  href="#"
                  className="flex items-center inset-y-0 cursor-pointer hover:text-gray-200 footer-link"
                >
                  <p className="text-sm">Facebook</p>
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div className="px-24">
        <p className="flex items-center justify-between text-gray-200 text-xs text-center border-t-[0.2px] border-t-slate-50/20 py-28">
          <span>©2024 Multiverse. All rights reserved</span>
          <span>
            <Link href="#" className="underline">
              Privacy Policy
            </Link>
          </span>
          <span>
            <Link href="#">Terms and Conditions</Link>
          </span>
        </p>
      </div>
    </footer>
  );
};

export default Footer;
