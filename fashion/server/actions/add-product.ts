import { productAddformSchema } from "@/lib/formSchema";
import { revalidatePath } from "next/cache";
import { createSafeActionClient } from "next-safe-action";

export const action = createSafeActionClient();

export const addProduct = action(productAddformSchema, async (content) => {
  
  const response = await fetch("/api/products", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(content),
  });
  const re = await response.json();
  console.log(re);

  if (!response.ok) {
    return { error: "Could not create product" };
  }

  revalidatePath("/products");
  const result = await response.json();
  return { success: result };
});

export const fetchProducts = async () => {
  const response = await fetch("http://localhost:3000/api/products");
  return response.json();
};

export const fetchFilters = async () => {
  const response = await fetch("/api/filters");
  return response.json();
};
