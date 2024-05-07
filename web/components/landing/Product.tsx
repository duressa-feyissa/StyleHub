"use client";

import Products from "./Products";

export default function Product({ list = false, cols = 3 }) {
  return (
    <div className="">
      <div className="mx-auto px-4 py-16 sm:px-6 sm:py-24 lg:container lg:px-8">
        <h2 className="text-4xl font-bold tracking-tight">
          POPULAR
        </h2>
        <Products list={list} cols={cols} />
      </div>
    </div>
  );
}
