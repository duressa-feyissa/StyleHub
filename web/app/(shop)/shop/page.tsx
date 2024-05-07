import { redirect } from "next/navigation";

export default async function LayoutGridDemo() {
  redirect("/shops");
  return <div className="h-screen py-20 w-full">Redirect to shops</div>;
}
