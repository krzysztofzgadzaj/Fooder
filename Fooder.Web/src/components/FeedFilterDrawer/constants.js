export const SortKeys = {
  priceAscending: 0,
  priceDescending: 1,
  nameAscending: 2,
  nameDescending: 3,
  averageRatingAscending: 4,
  averageRatingDescending: 5
};

export const SortModes = [
  {
    label: "Price: ascending",
    key: SortKeys.priceAscending
  },
  {
    label: "Price: descending",
    key: SortKeys.priceDescending
  },
  {
    label: "Name: A-Z",
    key: SortKeys.nameAscending
  },
  {
    label: "Name: Z-A",
    key: SortKeys.nameDescending
  },
  {
    label: "Rating: ascending",
    key: SortKeys.averageRatingAscending
  },
  {
    label: "Rating: descending",
    key: SortKeys.averageRatingDescending
  }
];
