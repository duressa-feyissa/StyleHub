import Image from "next/image";

interface ProductCardProps {
  name: string;
  href: string;
  imageSrc: string;
  imageAlt: string;
  price: string;
  location: string;
}

export default function ProductCard({name, href, imageSrc, imageAlt, price, location}: ProductCardProps) {
  return (
    <div className="group relative">
      <div className="aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-surfaceContainerLow lg:aspect-none group-hover:opacity-75 lg:h-80">
        <Image
          src={imageSrc}
          alt={imageAlt}
          className="h-full w-full object-cover object-center lg:h-full lg:w-full"
          width={500}
          height={500}
        />
      </div>
      <div className="mt-4 flex justify-between">
        <div>
          <h3 className="text-lg font-medium text-gray-700">
            <a href={href}>
              <span aria-hidden="true" className="absolute inset-0" />
              {name}
            </a>
          </h3>
          <p className="text-lg  font-bold text-primary">ETB {price}</p>
          <p className="mt-1 text-sm text-gray-500">{location}</p>
        </div>
      </div>
    </div>
  );
}
