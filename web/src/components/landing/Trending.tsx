import Product from "@/components/cards/Product";
import image from "../../../public/products/Elegant Beige Blazer For Women.png";

export default function Trending() {
  return (
    <div className="px-[160px] flex flex-col py-[64px] gap-y-x-large">
      <div className="flex flex-row justify-between h-[120px]">
        <p className="prose-display-medium-bold">{"Trending".toUpperCase()}</p>
      </div>
      <div className="grid grid-cols-3 gap-x-x-large gap-y-x-large">
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
        <Product
          title="Elegant Beige Blazer For Women"
          price={23}
          image={image}
        />
      </div>
    </div>
  );
}
