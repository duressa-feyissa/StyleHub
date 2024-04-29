import {
  BrandType,
  CategoryType,
  ColorType,
  MaterialType,
  SizeType,
} from "@/lib/type";
import { type NextRequest } from "next/server";

export async function GET(request: NextRequest) {
  const response0 = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Brand`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  const brands: BrandType[] = await response0.json();

  const response1 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Category`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  const categories: CategoryType[] = await response1.json();

  const response2 = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Color`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  const colors: ColorType[] = await response2.json();

  const response3 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Location`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  const locations: Location[] = await response3.json();

  const response4 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Material`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  const materials: MaterialType[] = await response4.json();

  const response5 = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Size`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  const sizes: SizeType[] = await response5.json();

  return Response.json({
    brands,
    categories,
    colors,
    locations,
    materials,
    sizes,
  });
}
