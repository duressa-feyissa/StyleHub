import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { type NextRequest } from "next/server";

export async function GET(request: NextRequest) {
  const session = request.cookies.get("token")?.value || "{}";
  const token = JSON.parse(session).token;

  const response = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Image`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  });

  const result = await response.json();
  return Response.json(result);
}

export async function POST(request: Request) {
  const body = await request.json();
  const cookiesStore = cookies();
  const session = cookiesStore.get("session")?.value || "{}";
  const token = JSON.parse(session).token;

  const response = await fetch(`${process.env.BACKEND_SERVER_URL}/api/Image`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    body: JSON.stringify(body),
  });

  console.log(response);

  const result = await response.json();
  console.log(result);
  return Response.json(result);
}
