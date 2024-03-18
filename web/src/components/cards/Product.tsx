import Image, { StaticImageData } from "next/image";

interface ProductProps {
  image: StaticImageData;
  title: string;
  price: number;
}

export default function Product({ image, title, price }: ProductProps) {
  return (
    <div className="flex flex-col w-[500px]">
      <div className=" bg-primaryContainer">
        <Image src={image} alt={title} />
      </div>
      <div className="p-x-small flex flex-col space-y-x-small">
        <p className=" prose-title-large text-onPrimaryContainer">{title}</p>
        <div className="flex flex-row space-xx-small space-x-x-small">
          <p className=" prose-body-large text-onPrimaryContainer">Price: </p>
          <span className="prose-title-medium text-primary">${price}</span>
        </div>
      </div>
    </div>
  );
}
