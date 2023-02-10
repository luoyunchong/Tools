 
<template>
  <n-grid x-gap="12" :cols="2">
    <n-gi>
      <n-card title="国密SM4">
        <n-list bordered="false">
          <n-list-item>
            <n-input v-model:value="data.text" type="textarea" placeholder="请输入要进行加密/解密的内容" />
          </n-list-item>
          <n-list-item>
            <n-input v-model:value="data.keys" type="text" placeholder="Keys" />
          </n-list-item>
          <n-list-item>
            <n-space>
              <n-button type="success" @click="convertEncode">
                加密
              </n-button>
              <n-button type="info" @click="convertDecode">
                解密
              </n-button>
            </n-space>
          </n-list-item>
          <n-list-item>
            <n-input-group>
              <n-input v-model:value="data.sm4base64" type="textarea" placeholder="转换结果" />
            </n-input-group>
          </n-list-item>
          <n-list-item>
            <n-button type="primary" ghost @click="copy(data.sm4base64)">
              复制结果
            </n-button>
          </n-list-item>
          <n-list-item>
            <n-input-group>
              <n-input v-model:value="data.sm4hex" type="textarea" placeholder="转换结果" />
            </n-input-group>
          </n-list-item>
          <n-list-item>
            <n-button type="primary" ghost @click="copy(data.sm4hex)">
              复制结果
            </n-button>
          </n-list-item>
        </n-list>
      </n-card>
    </n-gi>
    <n-gi>
      <div class="green" />
    </n-gi>
  </n-grid>

</template>



<script lang="ts" setup>
import API from '@/api';

const data = reactive({
  text: '',
  keys: '',
  sm4base64: '',
  sm4hex: '',
  ischeck: false
})

const convertEncode = () => {
  API.EcbPadding({ data: data.text, keys: data.keys, enOrDecrpyt: true }).then((res: SM4ECBOutput) => {
    data.sm4base64 = res.sm4Base64
    data.sm4hex = res.sm4Hex
  });
}

const convertDecode = () => {
  API.EcbPadding({ data: data.text, keys: data.keys, enOrDecrpyt: false }).then((res: SM4ECBOutput) => {
    data.sm4base64 = res.text
  });
}


const message = useMessage()
const copy = (text: string) => {
  navigator.clipboard.writeText(text);
  message.success("复制成功")
}


</script>