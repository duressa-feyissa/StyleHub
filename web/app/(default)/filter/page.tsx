"use client";

import Product from "@/components/landing/Product";
import { useState } from "react";
const sortOptions = [
  { name: "Most Popular", href: "#", current: true },
  { name: "Best Rating", href: "#", current: false },
  { name: "Newest", href: "#", current: false },
  { name: "Price: Low to High", href: "#", current: false },
  { name: "Price: High to Low", href: "#", current: false },
];
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { ToggleGroup, ToggleGroupItem } from "@/components/ui/toggle-group";
import { Button } from "@/components/ui/button";
import { Toggle } from "@radix-ui/react-toggle";
import { LayoutGrid, List, MapPin, Slash } from "lucide-react";
import { availability, conditions } from "./brands";
import { Checkbox } from "@/components/ui/checkbox";
import { Slider } from "@/components/ui/slider";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";
import { CategoriesView } from "@/components/category/category-list";
import {
  FilterType,
  BrandType,
  LocationType,
  CategoryType,
  ColorType,
  MaterialType,
  SizeType,
} from "@/lib/type";
import { useGetFilters } from "@/lib/data/get-filters";
import Products from "@/components/landing/Products";

export default function Filter() {
  const [selectedOption, setSelectedOption] = useState("");
  const [isToggled, setIsToggled] = useState(true);
  const { data: filters, error: postError, fetchStatus } = useGetFilters();
  if (postError || !filters) {
    return postError?.message;
  }
  const { brands, colors, categories, materials, sizes } = filters;

  const handleOptionClick = (optionName: string) => {
    setSelectedOption(optionName);
  };

  const handleToggle = () => {
    setIsToggled(!isToggled);
  };

  return (
    <div className="lg:container mb-24">
      <CategoriesView />
      <div className="border-b-2 w-full"></div>
      <div className="flex w-full justify-between items-center py-10">
        <div>
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem>
                <BreadcrumbLink href="/">Home</BreadcrumbLink>
              </BreadcrumbItem>
              <BreadcrumbSeparator>
                <Slash />
              </BreadcrumbSeparator>
              <BreadcrumbItem>
                <BreadcrumbLink href="/product">Products</BreadcrumbLink>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
        </div>
        <div className="flex gap-2 items-center">
          {" "}
          Find Products in
          <Button className="bg-onSurfaceVariant space-x-2">
            <MapPin />
            <p>Ethiopia</p>
          </Button>
        </div>
      </div>
      <div className="grid w-full grid-cols-4">
        <div className="col-span-1 flex flex-col gap-10">
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Brand</p>
            <ScrollArea className="max-h-40">
              {brands.map((brand: BrandType) => (
                <div
                  className="flex items-center space-x-2 mb-5"
                  key={brand.id}
                >
                  <Checkbox id={brand.id} />
                  <label
                    htmlFor={brand.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {brand.name}
                  </label>
                </div>
              ))}
            </ScrollArea>
          </div>
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Colors</p>
            <ScrollArea className="max-h-40">
              {colors.map((color: ColorType) => (
                <div
                  className="color-item flex items-center space-x-2 mb-5"
                  key={color.id}
                >
                  <div
                    className="w-4 h-4 border border-opacity-25 border-onSurface rounded-full"
                    style={{ backgroundColor: color.hexCode }}
                  ></div>
                  <label
                    htmlFor={color.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {color.name}
                  </label>
                </div>
              ))}
            </ScrollArea>
          </div>
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Material</p>
            <ScrollArea className="max-h-40">
              {materials.map((material: MaterialType) => (
                <div
                  className="flex items-center space-x-2 mb-5"
                  key={material.id}
                >
                  <Checkbox id={material.id} />
                  <label
                    htmlFor={material.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {material.name}
                  </label>
                </div>
              ))}
            </ScrollArea>
          </div>
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Size</p>
            <ScrollArea className="max-h-40">
              {sizes.map((size: SizeType) => (
                <div className="flex items-center space-x-2 mb-5" key={size.id}>
                  <Checkbox id={size.id} />
                  <label
                    htmlFor={size.name}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                  >
                    {size.abbreviation}
                  </label>
                </div>
              ))}
            </ScrollArea>
          </div>
          <div className="flex flex-col gap-5 w-[60%]">
            <p className="text-md font-semibold">Price</p>
            <Slider defaultValue={[33]} max={100} step={1} />
          </div>
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Availability</p>
            {availability.map((item) => (
              <div className="flex items-center space-x-2" key={item}>
                <Checkbox id={item} />
                <label
                  htmlFor={item}
                  className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                >
                  {item}
                </label>
              </div>
            ))}
          </div>
          <div className="flex flex-col gap-5">
            <p className="text-md font-semibold">Condition</p>
            {conditions.map((condition) => (
              <div className="flex items-center space-x-2" key={condition}>
                <Checkbox id={condition} />
                <label
                  htmlFor={condition}
                  className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70 capitalize"
                >
                  {condition}
                </label>
              </div>
            ))}
          </div>
        </div>
        <div className="col-span-3">
          <div className="flex justify-between items-center lg:container mb-10">
            <div>
              <Toggle onClick={handleToggle} className="p-2 bg-primary-foreground rounded-sm">
                {isToggled ? (
                  <List className="w-6 h-6" />
                ) : (
                  <LayoutGrid className="w-6 h-6" />
                )}
              </Toggle>
            </div>
            <div>
              <Select>
                <SelectTrigger className="w-[180px]">
                  <SelectValue placeholder="Sort By" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="name">Name</SelectItem>
                  <SelectItem value="type">Type</SelectItem>
                  <SelectItem value="size">Size</SelectItem>
                  <SelectItem value="condition">Condition</SelectItem>
                </SelectContent>
              </Select>
            </div>
          </div>
          <div className="flex">
            <ScrollArea className="w-full">
              <ToggleGroup type="multiple">
                {categories.map((item: CategoryType) => (
                  <ToggleGroupItem
                    key={item.id}
                    value="bold"
                    aria-label="Toggle bold"
                    className="text-nowrap bg-secondary"
                  >
                    {item.name}
                  </ToggleGroupItem>
                ))}
              </ToggleGroup>
              <ScrollBar orientation="horizontal" />
            </ScrollArea>
          </div>
          <Products list={!isToggled} cols={3} />
        </div>
      </div>
    </div>
  );
}
