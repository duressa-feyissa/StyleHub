import Product from "@/components/cards/Product";
import image from "../../../public/products/White Hoodle Cutout.png.png";
import FeatureChip from "./FeatureChip";

export default function Features() {
  const featureChips = ["All", "Men", "Women", "Kids", "Accessories"];
  return (
    <div className="px-[160px] flex flex-col py-[64px] gap-y-x-large">
      <div className="flex flex-row justify-between items-center h-[120px]">
        <p className="prose-display-medium-bold">
          {"FEATURED ITEMS".toUpperCase()}
        </p>
        <div>
          <div className="flex flex-row space-x-medium">
            {featureChips.map((label, index) => (
              <>
                {index === featureChips.length - 1 ? (
                  <FeatureChip
                    key={index}
                    label={label}
                    isSelected={label === "All"}
                  />
                ) : (
                  <>
                    <FeatureChip
                      key={index}
                      label={label}
                      isSelected={label === "All"}
                    />
                    <p
                      key={index}
                      className="prose-headline-small text-onSurface "
                    >
                      {":"}
                    </p>
                  </>
                )}
              </>
            ))}
          </div>
        </div>
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
