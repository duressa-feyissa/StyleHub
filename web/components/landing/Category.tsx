import CategoryCard from "./CategoryCard";
const image = "/products/2.png";

const Category = () => {
  return (
    <div className="">
      <div className="mx-auto px-4 py-16 sm:px-6 sm:py-24 lg:container lg:px-8">
        <h2 className="text-4xl font-bold tracking-tight">CATEGORIES</h2>

        <div className="mt-6 grid grid-cols-2 gap-x-6 gap-y-10 sm:grid-cols-2 md:grid-cols-4 lg:grid-cols-5 xl:gap-x-8">
          {[1, 2, 3, 4, 5, 6, 7, 8, 9, 0].map((item, index) => (
            <CategoryCard
              key={item}
              image={`/products/${index + 1}.png`}
              title="category"
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Category;
