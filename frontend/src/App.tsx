import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Copy, Check, Loader2, AlertCircle } from 'lucide-react'

interface ConvertResponse {
  words: string
}

interface FormValues {
  input: string
}

function App() {
  const [result, setResult] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [copied, setCopied] = useState(false)

  const form = useForm<FormValues>({
    defaultValues: {
      input: '',
    },
  })

  const onSubmit = async (data: FormValues) => {
    if (!data.input.trim()) return

    setLoading(true)
    setError(null)
    setResult(null)

    try {
      const response = await fetch(
        `http://127.0.0.1:5216/convert/${encodeURIComponent(data.input)}`
      )

      if (!response.ok) {
        const errorData = await response.json()
        throw new Error(errorData.error)
      }

      const responseData: ConvertResponse = await response.json()
      setResult(responseData.words)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unexpected error occurred')
    } finally {
      setLoading(false)
    }
  }

  const handleCopy = async () => {
    if (!result) return

    try {
      await navigator.clipboard.writeText(result)
      setCopied(true)
      setTimeout(() => setCopied(false), 2000)
    } catch (err) {
      console.error('Failed to copy text:', err)
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 to-blue-50 flex items-center justify-center p-4">
      <div className="w-full max-w-md space-y-6">
        <div className="text-center">
          <h1 className="text-3xl font-bold tracking-tight">Converter</h1>
          <p className="text-muted-foreground">Enter a currency amount to convert</p>
        </div>

        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="input"
              rules={{
                required: 'Please enter a number to convert',
                pattern: {
                  value: /^[0-9.]*$/,
                  message: 'Only numbers and dots are allowed',
                },
                validate: value => {
                  const num = parseFloat(value)
                  if (isNaN(num)) {
                    return 'Please enter a valid number'
                  }
                  return true
                },
              }}
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Number</FormLabel>
                  <FormControl>
                    <Input
                      type="text"
                      placeholder="Enter a number to convert..."
                      disabled={loading}
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <Button
              type="submit"
              disabled={loading || !form.watch('input')?.trim()}
              className="w-full"
            >
              {loading ? (
                <>
                  <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                  Converting...
                </>
              ) : (
                'Convert'
              )}
            </Button>
          </form>
        </Form>

        {error && (
          <div className="border border-destructive/50 bg-destructive/10 p-4 rounded-lg flex items-start space-x-3">
            <AlertCircle className="h-5 w-5 text-destructive mt-0.5 flex-shrink-0" />
            <div>
              <h3 className="text-sm font-medium text-destructive">Error</h3>
              <p className="text-sm text-destructive/80 mt-1">{error}</p>
            </div>
          </div>
        )}

        {result && (
          <div className="border border-border bg-card p-4 rounded-lg space-y-3">
            <div className="flex items-center justify-between">
              <h3 className="text-sm font-medium">Result</h3>
              <Button
                variant="outline"
                size="sm"
                onClick={handleCopy}
                className="flex items-center space-x-2"
              >
                {copied ? (
                  <>
                    <Check className="h-4 w-4" />
                    <span>Copied!</span>
                  </>
                ) : (
                  <>
                    <Copy className="h-4 w-4" />
                    <span>Copy</span>
                  </>
                )}
              </Button>
            </div>
            <div className="border border-border bg-background rounded p-3">
              <p className="text-sm break-words">{result}</p>
            </div>
          </div>
        )}
      </div>
    </div>
  )
}

export default App
