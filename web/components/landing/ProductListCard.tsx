import Image from "next/image";
import { AtSign, Facebook, MapPin, SendIcon, Twitter } from "lucide-react";

export default function page() {
  return (
    <div className="flex gap-x-5">
      <Image src="/hero/Image-2.png" alt="Image-1" width={250} height={250} />
      <div className="flex flex-col items-start justify-center gap-y-5 w-full">
        <div className="flex flex-col justify-start items-start">
          <p className="text-lg font-semibold">White Hoodle Cutout</p>
          <p className="text-sm text-Outline">
            The White Hoodie Cutout is a modern twist on a classic wardrobe
            staple, featuring unique cutout details that add an edgy and
            contemporary flair to your casual look.
          </p>
        </div>
        <div>
          <p className="text-xl text-pink-600">ETB 34</p>
        </div>
        <div className="flex flex-col gap-y-6 text-sm">
          <div className="flex gap-x-4  justify-center items-center">
            <p>Color</p>
            {["black", "green", "red"].map((color) => (
              <div
                key={color}
                className={`w-5 h-5 rounded-full border border-opacity-25 border-onSurface`}
                style={{ backgroundColor: color }}
              ></div>
            ))}
          </div>
        </div>
        <div className="flex gap-x-5 justify-center items-center">
          <p>Size</p>
          <div className="flex gap-x-5 items-center w-full">
            {["S", "M", "L", "XL", "XXL", "XXXL"].map((size) => (
              <div
                key={size}
                className="bg-surfaceContainerLow w-9 h-9 flex justify-center items-center rounded-full"
              >
                <p className="text-xs">{size}</p>
              </div>
            ))}
          </div>
        </div>
        <div className="flex gap-x-1 justify-start items-center">
          <MapPin className="w-5 h-5 opacity-75" />
          <p>Addis Ababa, Ethiopia</p>
        </div>
      </div>
    </div>
  );
}
