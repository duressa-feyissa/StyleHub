import { fetchFilters } from "@/server/actions/add-product";
import { useQuery } from "@tanstack/react-query";

export function useGetFilters() {
  return useQuery({
    queryFn: async () => fetchFilters(),
    queryKey: ["filters"],
  });
}
