import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { type NextRequest } from "next/server";

export async function GET(request: NextRequest) {
  const session = request.cookies.get("token")?.value || "{}";
  const token = JSON.parse(session).token;

  const response0 = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Brand`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  });

  const brands = await response0.json();

  const response1 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Category`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    }
  );

  const categories = await response1.json();

  const response2 = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Color`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  });

  const colors = await response2.json();

  const response3 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Location`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    }
  );

  const locations = await response3.json();

  const response4 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Material`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    }
  );

  const materials = await response4.json();

  const response5 = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Size`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    }
  );

  const sizes = await response5.json();

  return Response.json({ brands, categories, colors, locations, materials, sizes });
}
