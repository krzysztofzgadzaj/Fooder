<template>
  <v-navigation-drawer
    class="scrolable"
    width="350"
    app
    clipped
    permanent
    :color="theme.backgroundSecondaryColor"
  >
    <v-list-item class="dwarfed">
      <v-list-item-content>
        <v-select
          :color="theme.highlightColor"
          item-color="pink"
          outlined
          v-model="selectedKey"
          label="Sort"
          :items="sortModes"
          item-text="label"
          item-value="key"
        />
      </v-list-item-content>
    </v-list-item>
    <v-divider></v-divider>
    <v-list-item>
      <v-list-item-content>
        <v-list-item-title class="mx-3 fun-label header-label">
          Filter
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>
    <v-list-item>
      <v-list-item-content>
        <v-list-item-title class="mx-3 my-3 fun-label">
          Feed types:
        </v-list-item-title>
        <v-checkbox
          @change="onFeedTypeFilterChanged(type)"
          :color="theme.highlightColor"
          class="type-box"
          dense
          v-for="type in feedTypeFilterList"
          :key="type.name"
          v-model="type.isSelected"
        >
          <template v-slot:label>
            <span class="fun-label checkbox-label">{{ type.name }}</span>
          </template>
        </v-checkbox>
      </v-list-item-content>
    </v-list-item>
    <v-list-item>
      <v-list-item-content>
        <v-list-item-title class="mx-3 my-3 fun-label"
          >Price:
        </v-list-item-title>
        <v-row justify="center">
          <v-col cols="5">
            <v-text-field
              v-model="priceFrom"
              label="From"
              type="number"
              :color="theme.highlightColor"
            />
          </v-col>
          <v-col cols="5">
            <v-text-field
              v-model="priceTo"
              label="To"
              type="number"
              :color="theme.highlightColor"
            />
          </v-col>
        </v-row>
      </v-list-item-content>
    </v-list-item>
    <v-list-item v-if="userPets">
      <v-list-item-content>
        <v-list-item-title class="mx-3 my-3 fun-label"
          >Select feed that best suits your pet!
        </v-list-item-title>
        <v-row justify="center">
          <v-col cols="5">
            <v-chip-group
              v-if="userPets"
              column
              dark
              :color="theme.highlightColor"
              v-model="selectedPet"
            >
              <v-chip v-for="pet in userPets" :key="pet.id" return-object
                >{{ pet.name }}
              </v-chip>
            </v-chip-group>
          </v-col>
        </v-row>
      </v-list-item-content>
    </v-list-item>
  </v-navigation-drawer>
</template>

<script>
import { mapMutations, mapGetters, mapActions } from "vuex";
import { DefaultTheme } from "@/shared/constants";
import { SortModes, SortKeys } from "./constants";

export default {
  name: "FeedFilterDrawer",
  data() {
    return {
      theme: DefaultTheme,
      selectedKey: null,
      sortModes: SortModes,
      feedTypeFilterList: [],
      priceFrom: null,
      priceTo: null,
      selectedPet: null
    };
  },
  methods: {
    ...mapMutations("feedModule", ["setFilteredFeed"]),
    ...mapActions("feedModule", ["getFeedTypes"]),
    ...mapActions("petModule", ["getUserPets"]),
    createFeedTypeFilterList() {
      this.feedTypeFilterList = this.feedTypes.map(type => {
        return { isSelected: false, name: type.name, id: type.key };
      });
    },
    sort() {
      let comparator = undefined;
      switch (this.selectedKey) {
        case SortKeys.priceAscending:
          comparator = (a, b) => (a.price < b.price ? -1 : 1);
          break;
        case SortKeys.priceDescending:
          comparator = (a, b) => (b.price < a.price ? -1 : 1);
          break;
        case SortKeys.nameAscending:
          comparator = (a, b) =>
            a.name.toLowerCase().localeCompare(b.name.toLowerCase());
          break;
        case SortKeys.nameDescending:
          comparator = (a, b) =>
            b.name.toLowerCase().localeCompare(a.name.toLowerCase());
          break;
        case SortKeys.averageRatingAscending:
          comparator = (a, b) => (a.averageRating < b.averageRating ? -1 : 1);
          break;
        case SortKeys.averageRatingDescending:
          comparator = (a, b) => (b.averageRating < a.averageRating ? -1 : 1);
          break;
        default:
          break;
      }
      const sortedList = this.filteredFeed.sort(comparator);
      this.setFilteredFeed(sortedList);
    },
    filter() {
      let filteredFeed = this.filterConsideringFeedTypes(this.feed);
      filteredFeed = this.filterConsideringPriceFrom(filteredFeed);
      filteredFeed = this.filterConsideringPriceTo(filteredFeed);
      filteredFeed = this.filterConsideringUserPet(filteredFeed);
      this.setFilteredFeed(filteredFeed);
      this.sort();
    },
    filterConsideringFeedTypes(feed) {
      let filteredFeed = feed;
      const feedTypesFilters = this.feedTypeFilterList
        .filter(type => type.isSelected)
        .map(type => type.name);
      const areAnyFeedTypesSelected =
        Array.isArray(feedTypesFilters) && feedTypesFilters.length > 0;
      if (areAnyFeedTypesSelected) {
        filteredFeed = filteredFeed.filter(feed =>
          feedTypesFilters.includes(feed.feedType)
        );
      }
      return filteredFeed;
    },
    filterConsideringPriceFrom(feed) {
      let filteredFeed = feed;
      if (this.priceFrom) {
        filteredFeed = filteredFeed.filter(
          feed => feed.price >= this.priceFrom
        );
      }
      return filteredFeed;
    },
    filterConsideringPriceTo(feed) {
      let filteredFeed = feed;
      if (this.priceTo) {
        filteredFeed = filteredFeed.filter(feed => feed.price <= this.priceTo);
      }
      return filteredFeed;
    },
    filterConsideringUserPet(feed) {
      if (this.selectedPet === undefined || this.selectedPet === null) {
        return feed;
      } else {
        let filteredFeed = feed;
        const pet = this.userPets[this.selectedPet];
        const petAfflictions = pet.petAfflictions;
        filteredFeed = filteredFeed.filter(feed =>
          feed.dogAfflictions.every(x => !petAfflictions.includes(x))
        );
        filteredFeed = filteredFeed.filter(
          feed =>
            feed.minWeight <= pet.bodyWeight && feed.maxWeight >= pet.bodyWeight
        );
        const activityLevel = this.activityLevels.find(
          x => x.name === pet.activityLevel
        );
        filteredFeed = filteredFeed.filter(
          feed => feed.dogActivityLevel === activityLevel.key
        );

        return filteredFeed;
      }
    },
    onFeedTypeFilterChanged() {
      this.filter();
    }
  },
  computed: {
    ...mapGetters("feedModule", ["filteredFeed", "feed", "feedTypes"]),
    ...mapGetters("userModule", ["userInfo"]),
    ...mapGetters("petModule", ["userPets", "activityLevels"])
  },
  watch: {
    selectedKey() {
      this.sort();
    },
    priceFrom() {
      this.filter();
    },
    priceTo() {
      this.filter();
    },
    selectedPet() {
      this.filter();
    }
  },
  async mounted() {
    await this.getFeedTypes();
    if (this.userInfo) {
      await this.getUserPets(this.userInfo.id);
    }
    this.createFeedTypeFilterList();
  }
};
</script>

<style lang="scss" scoped>
.dwarfed {
  height: 120px;
  margin-bottom: -40px;
}
.header-label {
  font-size: 22px;
}
.scrolable {
  overflow-y: auto;
}
.type-box {
  margin-left: 15px;
  margin-top: -5px;
  margin-bottom: -5px;
}
.checkbox-label {
  font-size: 15px;
  margin-left: -5px;
  margin-top: 1px;
}
</style>
