import Link from "next/link";
import Logo from "./Logo";
import Search from "./Search";
import { Button } from "../ui/button";

const Navbar = () => {
  return (
    <div className="bg-surfaceContainerLow">
      <div className="lg:container flex p-2 justify-between">
        <Logo />
        <Search />
        <div className="flex gap-x-2">
          <Link href="/shops">
            <Button variant="outline">Shops</Button>
          </Link>
          <Link href="/signup">
            <Button>Sign Up</Button>
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
