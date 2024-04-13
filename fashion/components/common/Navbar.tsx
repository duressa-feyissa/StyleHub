import Link from "next/link";
import Logo from "./Logo";
import { Button } from "@radix-ui/themes";
import Search from "./Search";

const Navbar = () => {
  return (
    <div className="lg:container flex p-2 justify-between">
      <Logo />
      <Search />
      <div className="flex gap-x-2">
        <Link href="/shops">
          <Button variant="outline">Shops</Button>
        </Link>
        <Link href="/signup">
          <Button color="pink">Sign Up</Button>
        </Link>
      </div>
    </div>
  );
};

export default Navbar;
