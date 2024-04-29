import Providers from "@/lib/query-provider";
import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Stylehub Admin Dashboard",
  description: "Stylehub Admin Dashboard",
};

export default function AdminLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return <Providers>{children}</Providers>;
}
