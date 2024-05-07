import { Search } from "lucide-react";
import { Input } from "../ui/input";

const SearchBox = () => {
  return (
    <div className="hidden md:flex relative">
      <Search className="absolute left-2 top-2.5 h-4 w-4 text-muted-foreground" />
      <Input placeholder="Search" className="pl-8 border" />
    </div>
  );
};

export default SearchBox;
