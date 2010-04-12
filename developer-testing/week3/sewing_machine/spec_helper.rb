Spec::Matchers.define :be_of_size do |size_expected|
  match do |size_actual|
    (size_actual.width == size_expected.width) && (size_actual.height == size_expected.height)
  end

  failure_message_for_should do |size_actual|
    "Wrong size, expected #{size_expected.to_s}, got #{size_actual.to_s}"
  end

  description do
    "Testing sizes"
  end
end

