import Link from "next/link";

const Logo = () => {
  return (
    <Link href="/" className="flex font-Roboto font-extrabold items-center gap-x-2">
      <div className="bg-black text-white text-2xl w-10 h-10 flex items-center justify-center rounded">
        E
      </div>
      <div className="text-primary text-3xl">FASHION</div>
    </Link>
  );
};

export default Logo;
