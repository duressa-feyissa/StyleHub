import Button from "@/components/common/Button";
import Image from "next/image";
import image from "../../../../public/banner/banner-sliders-2.png";
export default function SlideTwo() {
  return (
    <div className=" flex flex-row w-full h-[800px] box-border px-[200px] py-xx-large overflow-hidden bg-[#FFFDEC] justify-between">
      <div className="flex flex-col gap-y-xx-large justify-center items-start w-[800px] ">
        <div>
          <p className="prose-display-large-bold inline text-onPrimaryContainer ">
            Empower Your Style: Unlock Your{" "}
            <span className="text-primary prose-display-large-bold">
              Best Look
            </span>{" "}
            with Us
          </p>
        </div>

        <p className="prose-headline-small text-secondary  ">
          Elevate Your Wardrobe: Explore the Latest Fashion Trends and Must-Have
          Styles! Explore Now on StyleHub.
        </p>
        <Button
          label={"VIEW ALL".toLocaleUpperCase()}
          color="text-onPrimary"
          backgroundColor="bg-primary"
        />
      </div>
      <Image
        src={image}
        className="w-[530px] h-[750px]"
        alt="banner slider image"
      />
    </div>
  );
}
