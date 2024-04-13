import ProductCard from "./ProductCard";

const product = {
  id: 1,
  name: "Basic Tee",
  href: "#",
  imageSrc: "/products/5.png",
  imageAlt: "Front of men's Basic Tee in black.",
  price: "35",
  location: "Addis Ababa, 4 kilo",
};
// More products...

export default function Product() {
  return (
    <div className="bg-white">
      <div className="mx-auto px-4 py-16 sm:px-6 sm:py-24 lg:container lg:px-8">
        <h2 className="text-4xl font-bold tracking-tight text-gray-900">
          POPULAR
        </h2>

        <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
          {[1, 2, 3, 4, 5, 6, 7, 8].map((index) => (
            <ProductCard
              {...product}
              imageSrc={`/products/${index + 4}.png`}
              key={index}
            />
          ))}
        </div>
      </div>
    </div>
  );
}
