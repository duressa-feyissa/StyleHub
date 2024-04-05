import image from "../../../public/products/Elegant Beige Blazer For Women.png";
import Category from "../cards/Category";

export default function CategoryList() {
  const datas = [
    {
      title: "Apparel",
      image: image,
      color: "bg-[#FFECEC]",
    },
    {
      title: "Footwear",
      image: image,
      color: "bg-[#FFF4EC]",
    },
    {
      title: "Accessories",
      image: image,
      color: "bg-[#FFFDEC]",
    },
    {
      title: "Sportswear",
      image: image,
      color: "bg-[#FFF4EC]",
    },
    {
      title: "Formalwear",
      image: image,
      color: "bg-[#FFECEC]",
    },
  ];
  return (
    <div className="flex flex-col gap-y-x-large px-[160px] pt-vv-large pb-v-large">
      <p className="prose-display-medium-bold">{"CATEGORY".toUpperCase()}</p>
      <div className="flex flex-row space-x-x-large">
        {datas.map((data, index) => (
          <Category
            key={index}
            title={data.title}
            image={data.image}
            color={data.color}
          />
        ))}
      </div>
    </div>
  );
}
