import Link from "next/link";
import Button from "../Button";
import Logo from "./Logo";

const Header = () => {
  return (
    <div className="h-[100px] bg-onPrimary w-full flex flex-row  justify-between px-[160px] items-center ">
      <Logo />
      <div className="flex flex-row justify-center  items-center gap-large">
        <Link href="/products">
          <p className="prose-title-large text-secondary px-x-small">
            Products
          </p>
        </Link>
        <Link href="/campany">
          <p className="prose-title-large text-secondary px-x-small">Campany</p>
        </Link>
        <Link href="/contact">
          <p className="prose-title-large text-secondary px-x-small">Contact</p>
        </Link>
      </div>
      <div>
        <Button
          label="LOGIN"
          color="text-onPrimary"
          backgroundColor="bg-primary"
        />
      </div>
    </div>
  );
};

export default Header;
