import React from "react";
import Image from "next/image";

const Footer = () => {
  return (
    <div className="w-screen h-screen flex flex-col justify-center items-center bg-[#45464F] gap-32">
      <div className="flex justify-center items-center gap-10">
        <div className="gap-10">
          <div>
            <p className="text-white text-4xl font-bold">
              Let{"'"}s Stay In Touch
            </p>
            <p>
              Subscribe to our newsletter to receive latest articles to your
              inbox weekly.
            </p>
          </div>
          <div>
            <input type="text" placeholder="Email Address" />
            <button>Subscribe</button>
          </div>
        </div>
        <div>
          <p className="text-white text-4xl font-bold">Apps</p>
          <div>
            <a
              href="https://play.google.com/store/apps/details?id=com.stylehub"
              target="_blank"
              rel="noreferrer"
            >
              <Image
                src="/images/Ellipse.svg"
                width={26}
                height={26}
                alt="Google Play"
              />
            </a>
            <a
              href="https://apps.apple.com/us/app/stylehub/id1586611245"
              target="_blank"
              rel="noreferrer"
            >
              <Image
                src="/images/Ellipse.svg"
                width={26}
                height={26}
                alt="App Store"
              />
            </a>
          </div>
        </div>
      </div>
      <div>Header</div>
    </div>
  );
};

export default Footer;
