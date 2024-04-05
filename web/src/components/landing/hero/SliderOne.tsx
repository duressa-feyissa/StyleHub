import Button from "@/components/common/Button";
import Image from "next/image";
import image from "../../../../public/hero/image-1.png";

const SliderOne = () => {
  return (
    <div className=" w-full h-[860px] bg-surfaceContainerLow px-[160px] pt-large flex flex-row overflow-hidden justify-between">
      <div className=" w-grow h-[840px] ">
        <Image
          src={image}
          alt="slider"
          className="object-fill h-full w-full "
        />
      </div>
      <div className="flex flex-col gap-y-v-large justify-center items-start w-[800px] ">
        <div className="flex flex-col gap-y-x-small">
          <p className="prose-headline-small text-onPrimaryContainer ">
            Discover Limitless Style
          </p>
          <p className=" text-onPrimaryContainer font-bold font-Roboto text-[80px]">
            FASHION{" "}
            <span className="text-primary font-bold  text-[80px]  ">
              HEAVEN
            </span>{" "}
          </p>
          <p className="prose-title-large text-secondary ">
            Indulge in Endless Style Possibilities at StyleHub Fashion Haven,
            where curated collections and trendsetting designs await to elevate
            your wardrobe.
          </p>
        </div>

        <Button
          label={"EXPLORE NOW".toLocaleUpperCase()}
          color="text-onPrimary"
          backgroundColor="bg-primary"
        />
      </div>
    </div>
  );
};

export default SliderOne;
