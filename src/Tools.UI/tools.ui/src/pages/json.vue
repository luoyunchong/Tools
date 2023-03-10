 
<template>
  <n-card title="JSON格式化" class="h-full shadow-sm rounded-16px">
    <div class="m-b-10px">
      <n-space>
        <n-button type="primary" ghost @click="onCollapse()"
          >collapse all</n-button
        >
        <n-button type="primary" @click="onExpand">expand all</n-button>
        <n-button type="warning" @click="toggleMode">Toggle mode</n-button>
        <n-button type="info" @click="darkTheme = !darkTheme"
          >Change theme</n-button
        >
      </n-space>
    </div>
    <vue-jsoneditor
      height="600"
      :dark-theme="darkTheme"
      :mode="mode"
      v-model:json="jsonData"
      ref="editor"
      :queryLanguagesIds="queryLanguages"
      @change-mode="mode = $event"
    />
  </n-card>
</template>

<script lang="ts" setup>
import VueJsoneditor from "vue3-ts-jsoneditor";
import type { QueryLanguageId } from "vue3-ts-jsoneditor";
const jsonData = ref({
  array: [1, 2, 3],
  boolean: true,
  Null: null,
  number: 123,
  seconds: 0,
  object: { a: "b", c: "d" },
  string: "Hello World",
});
const mode = ref("text");
const queryLanguages = ref<QueryLanguageId[]>([
  "javascript",
  "lodash",
  "jmespath",
]);
const toggleMode = () => {
  if (mode.value === "tree") {
    mode.value = "text";
  } else {
    mode.value = "tree";
  }
};
const darkTheme = ref(false);

const editor = ref();

const message = useMessage();
const onCollapse = () => {
  if (mode.value === "tree") {
    editor.value.$collapseAll();
  } else {
    message.warning("Please switch to tree mode first");
  }
};
const onExpand = () => {
  if (mode.value === "tree") {
    editor.value.$expandAll();
  } else {
    message.warning("Please switch to tree mode first");
  }
};
</script>
