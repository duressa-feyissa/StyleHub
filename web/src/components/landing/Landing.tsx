import CategoryList from "./CategoryList";
import Features from "./Features";
import Trending from "./Trending";
import BannerLayout from "./banner/BannerLayout";
import Hero from "./hero/Hero";

export default function Landing() {
  return (
    <div>
      <Hero />
      <CategoryList />
      <Features />
      <BannerLayout />
      <Trending />
    </div>
  );
}
