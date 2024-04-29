import Image from "next/image";
import Link from "next/link";
import appStore from "../../public/logo/app-store.png";
import playStore from "../../public/logo/play-store.png";
import FooterLinkGrid from "./component/FooterLinkGrid";

export default function Footer() {
  return (
    <div className=" bg-onSurfaceVariant">
      <div className=" flex flex-col max-w-7xl lg:mx-auto">
        <div className=" flex flex-row justify-between py-24">
          <div className="flex flex-col gap-y-6 w-[300px] ">
            <div className="flex flex-col gap-y-3">
              <p className="prose-display-medium text-onPrimary text-2xl font-bold">
                Let&apos;s stay in touch
              </p>
              <p className="prose-body-large text-primaryContainer opacity-80 text-sm">
                Subscribe to our newsletter to receive latest articles to your
                inbox weekly.
              </p>
            </div>
            <div className="flex flex-row h-[50px] bg-onPrimary rounded-md">
              <div className="flex-grow">
                <input
                  type="text"
                  placeholder="Enter your email"
                  className="w-full h-full border-none bg-transparent text-secondary px-4"
                />
              </div>
              <div className="flex bg-primary w-[140px] justify-center items-center h-full rounded-md">
                <p className="prose-body-large text-onPrimary">Subscribe</p>
              </div>
            </div>
          </div>
          <div className="flex flex-col gap-y-5   ">
            <div className="flex flex-col gap-y-4">
              <p className="prose-display-medium text-onPrimary text-2xl font-bold">
                Apps
              </p>
            </div>
            <div className="flex flex-row gap-4 ">
              <Link href="#">
                <Image src={appStore} width={150} height={50} alt="App store" />
              </Link>
              <Link href="#">
                <Image
                  src={playStore}
                  width={150}
                  height={50}
                  alt="Play store"
                />
              </Link>
            </div>
          </div>
          <FooterLinkGrid />
        </div>
        <div className=" h-[1px] w-full bg-onPrimary opacity-10 " />
        <div className="flex flex-row justify-between py-16 ">
          <p className="text-md text-primaryContainer">
            ©2024 Innovate Fusion. All rights reserved
          </p>
          <p className="text-md text-primaryContainer ">Privacy & Policy</p>
          <p className="text-md text-primaryContainer ">Terms & Condition</p>
        </div>
      </div>
    </div>
  );
}
