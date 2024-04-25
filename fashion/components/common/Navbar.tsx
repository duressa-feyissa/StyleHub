import Link from "next/link";
import Logo from "./Logo";
import Search from "./Search";
import { Button } from "../ui/button";
import { ModeToggle } from "@/components/modetoggle";
import NavbarButton from "./navbarButton";

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
          <NavbarButton />
          <ModeToggle />
        </div>
      </div>
    </div>
  );
};

export default Navbar;
