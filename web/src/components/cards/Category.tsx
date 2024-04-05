import Image, { StaticImageData } from "next/image";

interface CategoryProps {
  image: StaticImageData;
  title: string;
  color: string;
}

export default function Category({ image, title, color }: CategoryProps) {
  return (
    <div
      className={`w-[285px] h-[330px] py-x-large px-large flex flex-col gap-y-small items-center justify-center ${color}`}
    >
      <p className="prose-title-large text-primary">{title}</p>
      <Image src={image} alt={title} />
    </div>
  );
}
