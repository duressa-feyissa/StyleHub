import { fetchProducts } from "@/server/actions/add-product";
import { useQuery } from "@tanstack/react-query";

export function useGetProducts() {
  return useQuery({
    queryFn: async () => fetchProducts(),
    queryKey: ["products"],
  });
}
