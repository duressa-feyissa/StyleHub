import Product from "@/components/landing/Product";
const sortOptions = [
  { name: "Most Popular", href: "#", current: true },
  { name: "Best Rating", href: "#", current: false },
  { name: "Newest", href: "#", current: false },
  { name: "Price: Low to High", href: "#", current: false },
  { name: "Price: High to Low", href: "#", current: false },
];
import brands from "./brands";
import { Checkbox } from "@/components/ui/checkbox";
import { Slider } from "@/components/ui/slider";
import { ScrollArea } from "@/components/ui/scroll-area";
import { CategoriesView } from "@/components/category/category-list";

export default function Filter() {
  return (
    <div className="lg:container">
      <CategoriesView />
      <div className="grid w-full grid-cols-4">
        <ScrollArea className="h-[90vh]">
          <div className="col-span-1 flex flex-col gap-4">
            <div className="flex flex-col gap-2">
              <p className="text-md ">Brand</p>
              {brands.slice(0, 4).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
            <div className="flex flex-col gap-2">
              <p className="text-md ">Colors</p>
              {brands.slice(4, 8).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
            <div className="flex flex-col gap-2">
              <p className="text-md ">Material</p>
              {brands.slice(0, 4).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
            <div className="flex flex-col gap-2">
              <p className="text-md ">Size</p>
              {brands.slice(0, 4).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
            <div className="flex flex-col gap-2 w-[60%]">
              <p className="text-md ">Price</p>
              <Slider defaultValue={[33]} max={100} step={1} />
            </div>
            <div className="flex flex-col gap-2">
              <p className="text-md ">Availability</p>
              {brands.slice(0, 3).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
            <div className="flex flex-col gap-2">
              <p className="text-md ">Condition</p>
              {brands.slice(3, 6).map((brand) => (
                <div className="flex items-center space-x-2" key={brand.name}>
                  <Checkbox id={brand.name} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </div>
          </div>
        </ScrollArea>
        <div className="col-span-3">
          <ScrollArea className="h-[86vh]">
            <Product />
          </ScrollArea>
        </div>
      </div>
    </div>
  );
}
